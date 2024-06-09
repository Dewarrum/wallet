<script lang="ts">
	import { getModalStore } from '@skeletonlabs/skeleton';
	import MajesticonsNoteTextPlusLine from '~icons/majesticons/note-text-plus-line';

	export let profileId: string;
	$: pId = profileId;

	const modalStore = getModalStore();

	function openModal() {
		modalStore.trigger({
			type: 'component',
			component: 'createTransaction',
			meta: {
				profileId: pId
			}
		});
	}
</script>

<button type="button" class="variant-filled btn" on:click={() => openModal()}>
	<span>
		<MajesticonsNoteTextPlusLine />
	</span>
	<span>Add transaction</span>
</button>
