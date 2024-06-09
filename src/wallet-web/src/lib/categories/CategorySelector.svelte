<script lang="ts">
	import {
		Autocomplete,
		ProgressBar,
		popup,
		type AutocompleteOption,
		type PopupSettings
	} from '@skeletonlabs/skeleton';
	import { getCategories } from './queries';
	import type { Category } from './models';

	export let category: Category | null;
	let selectedCategoryName: string;

	const categoryQuery = getCategories();
	const { data: categories } = $categoryQuery;

	const options = categories?.items.map((c) => ({
		label: c.name,
		value: c
	}));

	const categoryPopupSettings: PopupSettings = {
		event: 'focus-click',
		target: 'popupAutocomplete',
		placement: 'bottom'
	};

	function onCategorySelection(event: CustomEvent<AutocompleteOption<Category>>): void {
		selectedCategoryName = event.detail.label;
		category = event.detail.value;
	}
</script>

<input
	class="autocomplete input"
	type="search"
	bind:value={selectedCategoryName}
	placeholder="Search..."
	use:popup={categoryPopupSettings}
	required
/>
<div
	data-popup="popupAutocomplete"
	class="card max-h-48 w-full max-w-sm overflow-y-auto p-4"
	tabindex="-1"
>
	{#if $categoryQuery.isLoading}
		<ProgressBar />
	{:else if $categoryQuery.data}
		<Autocomplete bind:input={selectedCategoryName} {options} on:selection={onCategorySelection} />
	{/if}
</div>
