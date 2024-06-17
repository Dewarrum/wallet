<script lang="ts">
	import { Button } from '$lib/components/ui/button/index';
	import * as Card from '$lib/components/ui/card/index';
	import ArrowUpRight from 'lucide-svelte/icons/arrow-up-right';

	export let data;
	const { profiles } = data;
</script>

<Card.Root class="xl:col-span-2">
	<Card.Header class="flex flex-row items-center">
		<div class="grid gap-4 lg:w-1/3">
			<Card.Title>Profiles</Card.Title>
		</div>
		<Button href="/app/profiles/create" size="sm" class="ml-auto gap-1">
			Create new
			<ArrowUpRight class="h-4 w-4" />
		</Button>
	</Card.Header>
	<Card.Content
		>{#each profiles as profile}
			<div class="flex flex-row gap-4 p-4">
				<div class="flex flex-1 flex-col gap-2">
					<div class="flex items-center gap-2">
						<div class="flex-1">
							<p class="text-sm font-medium">{profile.name}</p>
							<p class="text-sm text-muted-foreground">{profile.description}</p>
						</div>
					</div>
					<p class="text-sm text-muted-foreground">{profile.currency}</p>
				</div>
			</div>
		{/each}</Card.Content
	>
</Card.Root>
