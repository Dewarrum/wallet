<script lang="ts">
	import { Button } from '$lib/components/ui/button/index';
	import * as Card from '$lib/components/ui/card/index';
	import ArrowUpRight from 'lucide-svelte/icons/arrow-up-right';
	import TransactionTable from '$lib/components/transactions/transaction-table.svelte';
	import * as Select from '$lib/components/ui/select';
	import { page } from '$app/stores';
	import { goto } from '$app/navigation';
	import type { Selected } from 'bits-ui';

	export let data;
	const { transactions, profiles } = data;

	let currentProfileId = $page.url.searchParams.get('profileId');
	let currentProfile = currentProfileId
		? profiles.find((p) => p.id === currentProfileId)
		: undefined;
	let selectedProfile: Selected<string> | undefined = currentProfile
		? { value: currentProfile.id, label: currentProfile.name }
		: undefined;

	function onProfileChange(s: Selected<string>) {
		selectedProfile = {
			value: s.value,
			label: s.label
		};

		goto(`/app/transactions?profileId=${s.value}`);
	}
</script>

<Card.Root class="xl:col-span-2">
	<Card.Header class="flex flex-row items-center">
		<div class="grid gap-4 lg:w-1/3">
			<Card.Title>Transactions</Card.Title>

			<Select.Root selected={selectedProfile} onSelectedChange={(e) => e && onProfileChange(e)}>
				<Select.Trigger class="w-[180px]">
					<Select.Value placeholder="Select profile" />
				</Select.Trigger>
				<Select.Content>
					{#each profiles as profile}
						<Select.Item value={profile.id} label={profile.name} />
					{/each}
				</Select.Content>
			</Select.Root>
		</div>
		<Button href="/app/transactions/create" size="sm" class="ml-auto gap-1">
			Create new
			<ArrowUpRight class="h-4 w-4" />
		</Button>
	</Card.Header>
	<Card.Content>
		<TransactionTable {transactions} />
	</Card.Content>
</Card.Root>
